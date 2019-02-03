
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
	status int NOT NULL,
	expiration timestamp,
	checked timestamp
)
WITH (
    OIDS=FALSE
) ;


DROP TABLE public.domainstatus;
CREATE TABLE public.domainstatus (
    id SERIAL NOT NULL PRIMARY KEY,
	domainid INTEGER REFERENCES domain(id),
	description VARCHAR(50) NOT NULL UNIQUE,
	kind SMALLINT NOT NULL,
	assetid INTEGER REFERENCES asset(id),
	parentassetaccountid INTEGER REFERENCES assetaccount(id)
)
WITH (
    OIDS=FALSE
);
