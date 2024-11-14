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
            UniqueID VARCHAR(10) PRIMARY KEY,
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
            UniqueID VARCHAR(10) PRIMARY KEY,
            UserID VARCHAR(10),
            FOREIGN KEY (UserID) REFERENCES "User"(UniqueID)
        );
        CREATE TABLE CartDetail (
            UniqueID VARCHAR(10) PRIMARY KEY,
            CartID VARCHAR(10),
            ProductID VARCHAR(10),
            Amount INT,
            FOREIGN KEY (ProductID) REFERENCES Product(UniqueID),
            FOREIGN KEY (CartID) REFERENCES Cart(UniqueID)
        );

        ALTER TABLE "User" ADD CONSTRAINT chk_curPoint CHECK (
            (rankID = 'R01' AND curPoint >= 0 AND curPoint < 100) OR
            (rankID = 'R02' AND curPoint >= 100 AND curPoint < 200) OR
            (rankID = 'R03' AND curPoint >= 200 AND curPoint <300) OR
            (rankID = 'R04' AND curPoint >= 300 AND curPoint < 400) OR
            (rankID = 'R05' AND curPoint >= 400)
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
