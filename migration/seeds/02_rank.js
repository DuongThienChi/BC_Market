/**
 * @param { import("knex").Knex } knex
 * @returns { Promise<void> }
 */
exports.seed = async function (knex) {
  // Deletes ALL existing entries
  await knex("rank").del();
  await knex("rank").insert([
    {
      uniqueid: "R01",
      name: "Bronze",
      point: 0,
    },
    {
      uniqueid: "R02",
      name: "Silver",
      point: 100,
    },
    {
      uniqueid: "R03",
      name: "Gold",
      point: 200,
    },
    {
      uniqueid: "R04",
      name: "Platinum",
      point: 300,
    },
    {
      uniqueid: "R05",
      name: "Diamond",
      point: 400,
    },
  ]);
};
