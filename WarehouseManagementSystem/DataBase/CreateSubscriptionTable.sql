CREATE TABLE subscription
(
    subscription_id      SERIAL PRIMARY KEY,
    subscriber_id        INT       NOT NULL,
    creator_id           INT       NOT NULL,
    subscription_tier_id INT       NOT NULL,
    start_date           TIMESTAMP NOT NULL,
    end_date             TIMESTAMP NOT NULL,

    CONSTRAINT fk_subscription_subscriber FOREIGN KEY (subscriber_id) REFERENCES users (user_id) ON DELETE CASCADE,
    CONSTRAINT fk_subscription_creator FOREIGN KEY (creator_id) REFERENCES users (user_id) ON DELETE CASCADE,
    CONSTRAINT fk_subscription_tier FOREIGN KEY (subscription_tier_id) REFERENCES subscription_tier (subscription_tier_id) ON DELETE CASCADE
);