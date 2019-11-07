CREATE USER 'ola-results-service'@'%' IDENTIFIED BY 'secret';
GRANT SELECT ON ola.* TO 'ola-results-service'@'%';