using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WarehouseManagementSystem.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var migration ="""
                           CREATE TABLE users
                           (
                               user_id serial PRIMARY KEY,
                               user_name TEXT NOT NULL,
                               email TEXT NOT NULL,
                               password TEXT NOT NULL,
                               salt TEXT NOT NULL
                           );
                           CREATE TABLE role (
                                                 role_id serial PRIMARY KEY,
                                                 name TEXT
                           );
                           CREATE TABLE user_role(
                                                     user_role_id serial,
                                                     user_id INT,
                                                     role_id INT,
                                                     primary key (user_role_id),
                                                     constraint fk_user_role_user
                                                         foreign key (user_id)
                                                             references users (user_id)
                                                             on delete cascade,
                                                     constraint fk_user_role_role
                                                         foreign key (role_id)
                                                             references role (role_id)
                                                             on delete cascade
                           );
                           CREATE TABLE subscription_tier
                           (
                               subscription_tier_id SERIAL PRIMARY KEY,
                               name                 VARCHAR(255)   NOT NULL,
                               price                DECIMAL(10, 2) NOT NULL,
                               description          TEXT           NULL,
                               creator_id           INT            NOT NULL,
                               CONSTRAINT fk_subscription_tier_creator FOREIGN KEY (creator_id) REFERENCES users (user_id) ON DELETE CASCADE
                           );
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
                           """;
            migrationBuilder.Sql(migration);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("");
        }
    }
}
