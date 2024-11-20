exports.up = async function (knex) {
  await knex.raw(
    ` 
      CREATE TABLE role (
      UniqueID VARCHAR(10) PRIMARY KEY,
      Name VARCHAR(50) NOT NULL,
      Description VARCHAR(100)
      );
  
      -- Bảng Category
      CREATE TABLE category (
      UniqueID VARCHAR(10) PRIMARY KEY,
      Name VARCHAR(50) NOT NULL,
      description VARCHAR(100)
      );
  
      -- Bảng User    
      CREATE TABLE "User" (
      UniqueID INT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
      Username VARCHAR(50) NOT NULL,
      Password VARCHAR(1000) NOT NULL,
      Email VARCHAR(100),
      CreateAt DATE,
      RoleID VARCHAR(10),
      FOREIGN KEY (RoleID) REFERENCES Role(UniqueID)
      );
  
      -- Bảng Product
      CREATE TABLE product (
      UniqueID INT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
      Name VARCHAR(100) NOT NULL,
      Description VARCHAR(150),
      Price Double precision NOT NULL,
      Stock INT,
      CateID VARCHAR(10),
      ImagePath VARCHAR(150),
      Status BOOLEAN,
      OrderQuantity INT,
      FOREIGN KEY (CateID) REFERENCES Category(UniqueID)
      );
  
      `
  );
};

exports.down = async function (knex) {
  await knex.raw(`
          DROP TABLE IF EXISTS Product;
          DROP TABLE IF EXISTS "User";
          DROP TABLE IF EXISTS Role;
          DROP TABLE IF EXISTS Category;
         
      `);
};
