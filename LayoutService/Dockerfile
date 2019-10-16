FROM maven:3.6-jdk-11-slim as maven
COPY ./pom.xml ./pom.xml
RUN mvn dependency:go-offline -B
COPY ./src ./src
RUN mvn package

FROM openjdk:11-jre-slim
WORKDIR /layout-service
COPY --from=maven target/layout-service.jar ./
EXPOSE 8080
ENTRYPOINT ["java","-jar","layout-service.jar"]
