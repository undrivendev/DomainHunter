
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
	expiration timestamp,
	checked timestamp
)
WITH (
    OIDS=FALSE
) ;


DROP TABLE public.domainstatus;
CREATE TABLE public.domainstatus (
    id SERIAL NOT NULL PRIMARY KEY,
	domainid INTEGER NOT NULL  REFERENCES domain(id),
	error                    boolean NOT NULL,
    nowhois                  boolean NOT NULL,
    registrarlock            boolean NOT NULL,
    ok                       boolean NOT NULL,
    serverhold               boolean NOT NULL,
    redemptionperiod         boolean NOT NULL,
    addperiod                boolean NOT NULL,
    autorenewperiod          boolean NOT NULL,
    inactive                 boolean NOT NULL,
    pendingcreate            boolean NOT NULL,
    pendingdelete            boolean NOT NULL,
    pendingrenew             boolean NOT NULL,
    pendingrestore           boolean NOT NULL,
    pendingtransfer          boolean NOT NULL,
    pendingupdate            boolean NOT NULL,
    renewperiod              boolean NOT NULL,
    serverdeleteprohibited   boolean NOT NULL,
    serverrenewprohibited    boolean NOT NULL,
    servertransferprohibited boolean NOT NULL,
    serverupdateprohibited   boolean NOT NULL,
    transferperiod           boolean NOT NULL,
	clientdeleteprohibited boolean NOT NULL,
    clienthold boolean NOT NULL,
    clientrenewprohibited boolean NOT NULL,
    clienttransferprohibited boolean NOT NULL,
    clientupdateprohibited boolean NOT NULL

)
WITH (
    OIDS=FALSE
);
