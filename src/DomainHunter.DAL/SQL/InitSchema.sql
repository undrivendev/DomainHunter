
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
	tld VARCHAR(10) NOT NULL,
	status smallint NOT NULL,
	expiration date,
	"timestamp" timestamp,
	UNIQUE(name, tld)
)
WITH (
    OIDS=FALSE
) ;