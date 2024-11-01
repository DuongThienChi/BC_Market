require("dotenv").config();

module.exports = {
  development: {
    client: "pg",
    connection: {
      host: process.env.PG_HOST,
      port: parseInt(process.env.PG_PORT), //Cổng mặc định của PostgreSQL là 5432
      user: process.env.PG_USER,
      password: process.env.PG_PASSWORD,
      database: process.env.PG_DATABASE,
    },
  },
};
