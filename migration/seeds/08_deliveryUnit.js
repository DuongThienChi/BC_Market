/**
 * @param { import("knex").Knex } knex
 * @returns { Promise<void> } 
 */
exports.seed = async function(knex) {
  // Deletes ALL existing entries
  await knex('deliveryunit').del()
  await knex('deliveryunit').insert([
    {name: 'Standard Delivery', price: 5.00},
    {name: 'Express Delivery', price: 10.00},
    {name: 'Overnight Delivery', price: 20.00},
    {name: 'Two-Day Delivery', price: 15.00},
    {name: 'Same-Day Delivery', price: 25.00},
    {name: 'International Delivery', price: 50.00},
    {name: 'Local Courier', price: 8.00},
    {name: 'Drone Delivery', price: 30.00},
    {name: 'Freight Delivery', price: 100.00},
    {name: 'Economy Delivery', price: 3.00}
  ]);
};
