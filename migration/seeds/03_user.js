/**
 * @param { import("knex").Knex } knex
 * @returns { Promise<void> }
 */
exports.seed = async function (knex) {
  await knex("User").del();
  await knex("User").insert([
    {
      uniqueid: "U01",
      username: "admin",
      password: "1234",
      email: "admin@example.com",
      createat: knex.fn.now(),
      roleid: "R01",
    }, // Admin
    {
      uniqueid: "U02",
      username: "manager",
      password: "1234",
      email: "manager@example.com",
      createat: knex.fn.now(),
      roleid: "R02",
    }, // Manager
    {
      uniqueid: "U03",
      username: "cashier",
      password: "1234",
      email: "cashier@example.com",
      createat: knex.fn.now(),
      roleid: "R04",
    }, // Cashier
    {
      uniqueid: "U04",
      username: "shopper",
      password: "1234",
      email: "shopper@example.com",
      createat: knex.fn.now(),
      roleid: "R03",
    }, // Shopper
  ]);

  // Chèn 20 người dùng mới
  const usersToInsert = [];
  for (let i = 1; i <= 20; i++) {
    usersToInsert.push({
      uniqueid: "U" + (i + 4).toString().padStart(2, "0"), // uniqueid từ U05 đến U23
      username: "shopper" + i,
      password: "1234",
      email: "shopper_" + i + "@example.com",
      createat: knex.fn.now(),
      roleid: "R03", // Role là Shopper
    });
  }

  await knex("User").insert(usersToInsert);
};
