/**
 * @param { import("knex").Knex } knex
 * @returns { Promise<void> }
 */
exports.seed = async function (knex) {
  await knex("role").del();

  await knex("role").insert([
    { uniqueid: "R01", name: "Admin", description: "Quản lý ứng dụng" },
    { uniqueid: "R02", name: "Manager", description: "Quản lý cửa hàng" },
    { uniqueid: "R03", name: "Shopper", description: "Người mua sắm" },
    { uniqueid: "R04", name: "Cashier", description: "Thu ngân" },
  ]);
};
