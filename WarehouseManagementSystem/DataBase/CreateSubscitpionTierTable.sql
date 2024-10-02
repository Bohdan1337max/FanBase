CREATE TABLE subscription_tier
(
    subscription_tier_id SERIAL PRIMARY KEY,
    name                 VARCHAR(255)   NOT NULL,
    price                DECIMAL(10, 2) NOT NULL,
    description          TEXT           NULL,
    creator_id           INT            NOT NULL,
    CONSTRAINT fk_subscription_tier_creator FOREIGN KEY (creator_id) REFERENCES users (user_id) ON DELETE CASCADE
);