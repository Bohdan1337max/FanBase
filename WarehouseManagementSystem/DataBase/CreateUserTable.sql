CREATE TABLE users
(
    user_id serial PRIMARY KEY,
    user_name TEXT NOT NULL,
    email TEXT NOT NULL,
    password TEXT NOT NULL,
    salt TEXT NOT NULL
);