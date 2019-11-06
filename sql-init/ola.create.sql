CREATE USER 'ola-results-service'@'%' IDENTIFIED BY 'secret';
GRANT CREATE,ALTER,SELECT,INSERT,UPDATE,DELETE ON ola.* TO 'ola-results-service'@'%';