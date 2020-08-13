CREATE DATABASE super_market_db;
GRANT ALL PRIVILEGES ON DATABASE super_market_db TO postgres;

\c super_market_db;

CREATE TABLE public.categories
(
    id serial NOT NULL PRIMARY KEY,
    name varchar(30) NOT NULL
);

ALTER TABLE public.categories
    OWNER to postgres;

CREATE TABLE public.products
(
    id serial NOT NULL PRIMARY KEY,
    name varchar(50) NOT NULL,
    quantityinpackage smallint NOT NULL,
    unitofmeasurement smallint NOT NULL,
    categoryid integer NOT NULL,
    CONSTRAINT fk_category FOREIGN KEY (categoryid)
        REFERENCES public.categories (id) MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE CASCADE
); 

ALTER TABLE public.products
    OWNER to postgres;