exports.up = async function (knex) {
  await knex.raw(
    `
        ALTER TABLE "User" ADD COLUMN rankID VARCHAR(10);
        ALTER TABLE "User" ADD COLUMN curPoint INT;
        CREATE TABLE Rank (
            UniqueID VARCHAR(10) PRIMARY KEY,
            Name VARCHAR(50) NOT NULL,
            Point INT
        );
        ALTER TABLE "User" ADD FOREIGN KEY (rankID) REFERENCES Rank(UniqueID);
        CREATE TABLE Voucher (
            UniqueID INT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
            Name VARCHAR(50) NOT NULL,
            Description VARCHAR(100),
            Percent VARCHAR(5),
            Amount INT,
            Condition double precision,
            Stock INT,
            Validate DATE,
            rankID VARCHAR(10),
            FOREIGN KEY (rankID) REFERENCES Rank(UniqueID)
        );
        CREATE TABLE Cart (
            UniqueID INT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
            UserID INT,
            FOREIGN KEY (UserID) REFERENCES "User"(UniqueID)
        );
        CREATE TABLE CartDetail (
            UniqueID INT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
            CartID INT,
            ProductID INT,
            Amount INT,
            FOREIGN KEY (ProductID) REFERENCES Product(UniqueID),
            FOREIGN KEY (CartID) REFERENCES Cart(UniqueID)
        );

    `
  );
};

exports.down = async function (knex) {
  await knex.raw(`
        ALTER TABLE "User" DROP COLUMN rankID;
        ALTER TABLE "User" DROP COLUMN curPoint;
        DROP TABLE IF EXISTS Rank;
        ALTER TABLE "User" DROP FOREIGN KEY (rankID);
        DROP TABLE IF EXISTS Voucher;
        DROP TABLE IF EXISTS Cart;
        DROP TABLE IF EXISTS CartDetail;
    `);
};
