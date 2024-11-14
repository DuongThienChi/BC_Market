/**
 * @param { import("knex").Knex } knex
 * @returns { Promise<void> }
 */
exports.seed = async function (knex) {
  // Deletes ALL existing entries
  await knex("cart").del();
  await knex("cart").insert([
    {
      uniqueid: "CART001",
      userid: "U05",
    },
    {
      uniqueid: "CART002",
      userid: "U06",
    },
    {
      uniqueid: "CART003",
      userid: "U07",
    },
    {
      uniqueid: "CART004",
      userid: "U08",
    },
    {
      uniqueid: "CART005",
      userid: "U09",
    },
    {
      uniqueid: "CART006",
      userid: "U10",
    },
    {
      uniqueid: "CART007",
      userid: "U11",
    },
    {
      uniqueid: "CART008",
      userid: "U12",
    },
    {
      uniqueid: "CART009",
      userid: "U13",
    },
    {
      uniqueid: "CART010",
      userid: "U14",
    },
    {
      uniqueid: "CART011",
      userid: "U15",
    },
    {
      uniqueid: "CART012",
      userid: "U16",
    },
    {
      uniqueid: "CART013",
      userid: "U17",
    },
    {
      uniqueid: "CART014",
      userid: "U18",
    },
    {
      uniqueid: "CART015",
      userid: "U19",
    },
    {
      uniqueid: "CART016",
      userid: "U20",
    },
    {
      uniqueid: "CART017",
      userid: "U21",
    },
    {
      uniqueid: "CART018",
      userid: "U22",
    },
    {
      uniqueid: "CART019",
      userid: "U23",
    },
    {
      uniqueid: "CART020",
      userid: "U24",
    },
  ]);
};
