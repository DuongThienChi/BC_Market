require("dotenv").config();

module.exports = {
  development: {
    client: "pg",
    connection: {
      host: '127.0.0.1',
      port: 5433, //Cổng mặc định của PostgreSQL là 5432
      user: 'postgres',
      password: 'Postgres@123',
      database: 'BCMarket',
    },
  },
};
