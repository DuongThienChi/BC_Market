/**
 * @param { import("knex").Knex } knex
 * @returns { Promise<void> }
 */
exports.up = async function(knex) {
    await knex.raw(
        `
    -- Bảng DeliveryUnit
      CREATE TABLE DeliveryUnit (
      Id SERIAL PRIMARY KEY,
      Name VARCHAR(255) NOT NULL,
      Price DECIMAL(10, 2) NOT NULL
      );

      CREATE TABLE "Order" (
      Id SERIAL PRIMARY KEY,
      userId VARCHAR(10),
      shipId INT,
      createAt TIMESTAMP,
      totalPrice DECIMAL(10, 2) ,
      address VARCHAR(255),
      paymentMethod VARCHAR(50) NOT NULL,
      isPaid BOOLEAN NOT NULL
      );

      CREATE TABLE OrderDetail (
      OrderId INT,
      productId VARCHAR(10),
      amount INT 
      );

      Alter Table "Order" Add Constraint "Order_UserId_fkey" Foreign Key (userId) References "User"(uniqueid);
      Alter Table "Order" Add Constraint "Order_shipId_fkey" Foreign Key (shipId) References DeliveryUnit(Id);
      Alter Table OrderDetail Add Constraint "OrderDetail_ProductId_fkey" Foreign Key (productId) References product(uniqueid);
      Alter Table OrderDetail Add Constraint "OrderDetail_OrderId_fkey" Foreign Key (OrderId) References "Order"(Id);
      Alter Table OrderDetail Add Constraint "OrderDetail_OrderId_productId_key" Primary Key (OrderId, productId);
        `
    )
  
};

/**
 * @param { import("knex").Knex } knex
 * @returns { Promise<void> }
 */
exports.down = async function(knex) {

    await knex.raw(
        `
        DROP TABLE IF EXISTS DeliveryUnit;
        DROP TABLE IF EXISTS "Order";
        DROP TABLE IF EXISTS OrderDetail;
        `
    )
  
};
