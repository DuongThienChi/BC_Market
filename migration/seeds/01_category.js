/**
 * @param { import("knex").Knex } knex
 * @returns { Promise<void> }
 */
exports.seed = async function (knex) {
  await knex("category").del();

  await knex("category").insert([
    {
      uniqueid: "M01",
      name: "Meat",
      description: "Thịt và các sản phẩm làm từ thịt",
    },
    {
      uniqueid: "SF01",
      name: "Seafood",
      description: "Thủy hải sản và các sản phẩm làm từ thủy hải sản",
    },
    {
      uniqueid: "Vet01",
      name: "Vegetable",
      description: "Trái cây, rau củ quả và các sản phẩm từ thực vật",
    },
    {
      uniqueid: "BH01",
      name: "Beauty & Health",
      description: "Mỹ phẩm và các sản phẩm chăm sóc sức khỏe",
    },
    {
      uniqueid: "Mk01",
      name: "Milk",
      description: "Sữa và các sản phẩm từ sữa",
    },
  ]);
};
