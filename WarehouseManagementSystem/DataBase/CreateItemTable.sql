CREATE TABLE item
(
    item_id serial PRIMARY KEY ,
    created_date TIMESTAMP NOT Null,
    name TEXT NOT NULL,
    description TEXT NULL
);
