/**
 * @param { import("knex").Knex } knex
 * @returns { Promise<void> } 
 */
exports.seed = async function(knex) {
  // Deletes ALL existing entries
  await knex('orderdetail').del()
  await knex('orderdetail').insert([
    {orderid: 1, productid: '1', amount: 2},
    {orderid: 2, productid: '2', amount: 3},
    {orderid: 3, productid: '3', amount: 1},
    {orderid: 4, productid: '4', amount: 5},
    {orderid: 5, productid: '5', amount: 2},
    {orderid: 6, productid: '6', amount: 4},
    {orderid: 7, productid: '7', amount: 3},
    {orderid: 8, productid: '8', amount: 1},
    {orderid: 9, productid: '9', amount: 2},
    {orderid: 10, productid: '10', amount: 5},
    {orderid: 11, productid: '11', amount: 3},
    {orderid: 12, productid: '12', amount: 4},
    {orderid: 13, productid: '13', amount: 2},
    {orderid: 14, productid: '14', amount: 1},
    {orderid: 15, productid: '15', amount: 5},
    {orderid: 16, productid: '16', amount: 3},
    {orderid: 17, productid: '17', amount: 4},
    {orderid: 18, productid: '18', amount: 2},
    {orderid: 19, productid: '19', amount: 1},
    {orderid: 20, productid: '20', amount: 5},
    {orderid: 21, productid: '21', amount: 3},
    {orderid: 22, productid: '22', amount: 4},
    {orderid: 23, productid: '23', amount: 2},
    {orderid: 24, productid: '24', amount: 1},
    {orderid: 25, productid: '25', amount: 5},
    {orderid: 26, productid: '26', amount: 3},
    {orderid: 27, productid: '27', amount: 4},
    {orderid: 28, productid: '28', amount: 2},
    {orderid: 29, productid: '29', amount: 1},
    {orderid: 30, productid: '30', amount: 5},
    {orderid: 1, productid: '31', amount: 3},
    {orderid: 2, productid: '32', amount: 4},
    {orderid: 3, productid: '33', amount: 2},
    {orderid: 4, productid: '34', amount: 1},
    {orderid: 5, productid: '35', amount: 5},
    {orderid: 6, productid: '36', amount: 3},
    {orderid: 7, productid: '37', amount: 4},
    {orderid: 8, productid: '38', amount: 2},
    {orderid: 9, productid: '39', amount: 1},
    {orderid: 10, productid: '40', amount: 5},
    {orderid: 11, productid: '41', amount: 3},
    {orderid: 12, productid: '42', amount: 4},
    {orderid: 13, productid: '43', amount: 2},
    {orderid: 14, productid: '44', amount: 1},
    {orderid: 15, productid: '45', amount: 5},
    {orderid: 16, productid: '1', amount: 3},
    {orderid: 17, productid: '2', amount: 4},
    {orderid: 18, productid: '3', amount: 2},
    {orderid: 19, productid: '4', amount: 1},
    {orderid: 20, productid: '5', amount: 5},
    {orderid: 21, productid: '6', amount: 3},
    {orderid: 22, productid: '7', amount: 4},
    {orderid: 23, productid: '8', amount: 2},
    {orderid: 24, productid: '9', amount: 1},
    {orderid: 25, productid: '10', amount: 5},
    {orderid: 26, productid: '11', amount: 3},
    {orderid: 27, productid: '12', amount: 4},
    {orderid: 28, productid: '13', amount: 2},
    {orderid: 29, productid: '14', amount: 1},
    {orderid: 30, productid: '15', amount: 5}
  ]);
};
