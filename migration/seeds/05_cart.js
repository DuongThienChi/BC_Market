/**
 * @param { import("knex").Knex } knex
 * @returns { Promise<void> }
 */
exports.seed = async function (knex) {
  // Deletes ALL existing entries
  await knex("cart").del();
  await knex("cart").insert([
    {
      userid: 5,
    },
    {
      userid: 6,
    },
    {
      userid: 7,
    },
    {
      userid: 8,
    },
    {
      userid: 9,
    },
    {
      userid: 10,
    },
    {
      userid: 11,
    },
    {
      userid: 12,
    },
    {
      userid: 13,
    },
    {
      userid: 14,
    },
    {
      userid: 15,
    },
    {
      userid: 16,
    },
    {
      userid: 17,
    },
    {
      userid: 18,
    },
    {
      userid: 19,
    },
    {
      userid: 20,
    },
    {
      userid: 21,
    },
    {
      userid: 22,
    },
    {
      userid: 23,
    },
    {
      userid: 24,
    },
  ]);
};
