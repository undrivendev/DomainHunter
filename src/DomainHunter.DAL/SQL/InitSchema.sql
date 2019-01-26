
DROP DATABASE domainhunter;
CREATE DATABASE domainhunter
    WITH 
    OWNER = postgres
    ENCODING = 'UTF8'
    TABLESPACE = pg_default
    CONNECTION LIMIT = -1;


DROP TABLE public.domain;
CREATE TABLE public.domain (
    id SERIAL NOT NULL PRIMARY KEY,
	name VARCHAR(100) NOT NULL,
	status smallint NOT NULL,
	"timestamp" timestamp
)
WITH (
    OIDS=FALSE
) ;