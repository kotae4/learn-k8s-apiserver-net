# kubernetes-experiment-apiserver

**A simple API served via asp.net core mvc.**

Learning about kubernetes core concepts with a simple, microservice-based application. It's very messy and a lot of stuff is hardcoded, but that's okay because it's just for learning.

The [learn-k8s-webapp](https://github.com/kotae4/learn-k8s-webapp) microservice, its own deployment in the k8s cluster, is a flask app that serves the frontend and talks to the API.<br>
The **learn-k8s-apiserver-net** microservice, a separate deployment in the cluster, is an asp.net app that handles requests from the webapp backend and talks to the database.<br>
The database is external (exists outside the cluster). This example will use a local mysql DB, but could be adapted to use AWS RDS or some other cloud provider's RDBMS.<br>

## Containerization

Networking:
```bash
# to begin:
docker network create learn-k8s-network
# once done:
docker network remove learn-k8s-network
```

Building:
```bash
docker build -t learn-k8s-apiserver:latest .
```

Running:
```bash
docker run --rm -d --name apiserver --network learn-k8s-network -p 27525:8080 -e ConnectionStrings__SQL_CONNECTIONSTRING="server=mysqldb;user=badmin;password=vagrant;database=appdb" learn-k8s-apiserver
```

Debugging (no --rm or -d):
```bash
docker run --name apiserver --network learn-k8s-network -p 27525:8080 -e ConnectionStrings__SQL_CONNECTIONSTRING="server=mysqldb;user=badmin;password=vagrant;database=appdb" learn-k8s-apiserver
```

Cleaning up:
```bash
docker stop apiserver
docker rm apiserver
```

## Notes on running

It expects a mysql 8.4.4 database to be hosted @ `mysqldb:3306` with username `badmin` and password `vagrant`. Configured in `appsettings.json` (or Development version), or via environment variable `ConnectionStrings__SQL_CONNECTIONSTRING`.

If you need to change the version of mysql, update `Database/DatabaseConfigurationExtensions.cs` too (preferably make it pull from config, oops.)

For local development, you can spin up a mysql database like this:
```bash
docker pull mysql:8.4.4
```

```bash
docker run -d --network learn-k8s-network -p 23306:3306 --name mysqldb --hostname mysqldb -e MYSQL_DATABASE=appdb -e MYSQL_USER=badmin -e MYSQL_PASSWORD=vagrant -e MYSQL_ROOT_PASSWORD=vagrant mysql:8.4.4
```

## Regenerating votingApi client

1. Run and open up <apiserver>/swagger/v1/swagger.json.
2. Download swagger-codegen-cli (change version number if needed, [see here](https://github.com/swagger-api/swagger-codegen))
    ```
    # wget available
    wget https://repo1.maven.org/maven2/io/swagger/codegen/v3/swagger-codegen-cli/3.0.50/swagger-codegen-cli-3.0.50.jar -O swagger-codegen-cli.jar
    # windows powershell (no wget)
    Invoke-WebRequest -OutFile swagger-codegen-cli.jar https://repo1.maven.org/maven2/io/swagger/codegen/v3/swagger-codegen-cli/3.0.50/swagger-codegen-cli-3.0.50.jar
    ```
3. Point your terminal at the `learn-k8s-apiserver-net` directory
4. `java -jar .\swagger-codegen-cli-3.0.47.jar generate -i openapi.json -l python -o votingApi -DpackageName=votingApi`
5. Copy the votingApi directory back into `learn-k8s-webapp` directory
6. Remove all code dealing with multiprocessing in `votingApi/api_client.py` if needed (AWS Lambdas)
7. Open `votingApi/configuration.py` and make sure the host matches expectations (default: `http://api.testing.private:27525`)

## Kubernetes Debugging

Start an interactive pod (busybox image has ping and netcat installed):
kubectl run -i --tty --rm debug --image=busybox --restart=Never --namespace learn-k8s-apiserver -- sh

Test MySQL connection:
kubectl run -i --tty --rm debugsql --image=mysql --restart=Never --namespace learn-k8s-apiserver -- mysql --host=externaldb --port=23306 --user=badmin --password=vagrant --database=appdb --execute="SHOW TABLES;"