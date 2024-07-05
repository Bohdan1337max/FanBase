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
)