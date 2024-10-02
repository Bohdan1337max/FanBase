CREATE TABLE post
(
    post_id       SERIAL PRIMARY KEY,
    title         VARCHAR(255) NOT NULL,
    description   TEXT         NOT NULL,
    image_url     VARCHAR(255),
    creation_time TIMESTAMP    NOT NULL,
    edit_time     TIMESTAMP,
    creator_id    INT          NOT NULL,

    CONSTRAINT fk_creator FOREIGN KEY (creator_id) REFERENCES users (user_id) ON DELETE CASCADE
);
