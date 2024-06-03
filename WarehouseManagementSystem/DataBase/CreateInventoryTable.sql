CREATE TABLE inventory
(
    inventory_id      serial,
    item_id           INT  NOT NULL,
    name              TEXT NOT NULL,
    description       TEXT,
    location          TEXT NOT NULL,
    quantity          INT  NOT NULL,
    reserved_quantity INT,
    primary key (inventory_id),
    constraint fk_inventory_item
        foreign key (item_id)
            references item (item_id)
            on delete cascade
)