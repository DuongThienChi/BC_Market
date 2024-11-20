/**
 * @param { import("knex").Knex } knex
 * @returns { Promise<void> } 
 */
exports.seed = async function(knex) {
  // Deletes ALL existing entries
  await knex("Order").del()
  await knex("Order").insert([
    {userid: 4, shipid: 1, createat: knex.fn.now(), totalprice: 100.00, address: '123 Main St', paymentmethod: 'Credit Card', ispaid: true},
    {userid: 5, shipid: 2, createat: knex.fn.now(), totalprice: 150.00, address: '456 Elm St', paymentmethod: 'PayPal', ispaid: false},
    {userid: 6, shipid: 3, createat: knex.fn.now(), totalprice: 200.00, address: '789 Oak St', paymentmethod: 'Credit Card', ispaid: true},
    {userid: 7, shipid: 4, createat: knex.fn.now(), totalprice: 250.00, address: '101 Pine St', paymentmethod: 'Debit Card', ispaid: false},
    {userid: 8, shipid: 5, createat: knex.fn.now(), totalprice: 300.00, address: '202 Maple St', paymentmethod: 'Credit Card', ispaid: true},
    {userid: 9, shipid: 6, createat: knex.fn.now(), totalprice: 350.00, address: '303 Birch St', paymentmethod: 'PayPal', ispaid: false},
    {userid: 10, shipid: 7, createat: knex.fn.now(), totalprice: 400.00, address: '404 Cedar St', paymentmethod: 'Credit Card', ispaid: true},
    {userid: 11, shipid: 8, createat: knex.fn.now(), totalprice: 450.00, address: '505 Walnut St', paymentmethod: 'Debit Card', ispaid: false},
    {userid: 12, shipid: 9, createat: knex.fn.now(), totalprice: 500.00, address: '606 Chestnut St', paymentmethod: 'Credit Card', ispaid: true},
    {userid: 13, shipid: 10, createat: knex.fn.now(), totalprice: 550.00, address: '707 Ash St', paymentmethod: 'PayPal', ispaid: false},
    {userid: 14, shipid: 1, createat: knex.fn.now(), totalprice: 600.00, address: '808 Fir St', paymentmethod: 'Credit Card', ispaid: true},
    {
      userid: 15, shipid: 2, createat: knex.fn.now(), totalprice: 650.00, address: '909 Spruce St', paymentmethod: 'Debit Card', ispaid: false
    },
    {
      userid: 16, shipid: 3, createat: knex.fn.now(), totalprice: 700.00, address: '1010 Redwood St', paymentmethod: 'Credit Card', ispaid: true
    },
    {
      userid: 17, shipid: 4, createat: knex.fn.now(), totalprice: 750.00, address: '1111 Sequoia St', paymentmethod: 'PayPal', ispaid: false
    },
    {
      userid: 18, shipid: 5, createat: knex.fn.now(), totalprice: 800.00, address: '1212 Cypress St', paymentmethod: 'Credit Card', ispaid: true
    },
    {
      userid: 19, shipid: 6, createat: knex.fn.now(), totalprice: 850.00, address: '1313 Palm St', paymentmethod: 'Debit Card', ispaid: false
    },
    {
      userid: 20, shipid: 7, createat: knex.fn.now(), totalprice: 900.00, address: '1414 Willow St', paymentmethod: 'Credit Card', ispaid: true
    },
    {
      userid: 21, shipid: 8, createat: knex.fn.now(), totalprice: 950.00, address: '1515 Magnolia St', paymentmethod: 'PayPal', ispaid: false
    },
    {
      userid: 22, shipid: 9, createat: knex.fn.now(), totalprice: 1000.00, address: '1616 Dogwood St', paymentmethod: 'Credit Card', ispaid: true
    },
    {
      userid: 23, shipid: 10, createat: knex.fn.now(), totalprice: 1050.00, address: '1717 Hickory St', paymentmethod: 'Debit Card', ispaid: false
    },
    {
      userid: 24, shipid: 1, createat: knex.fn.now(), totalprice: 1100.00, address: '1818 Poplar St', paymentmethod: 'Credit Card', ispaid: true
    },
    {
      userid: 4, shipid: 2, createat: knex.fn.now(), totalprice: 1150.00, address: '1919 Sycamore St', paymentmethod: 'PayPal', ispaid: false
    },
    {
      userid: 5, shipid: 3, createat: knex.fn.now(), totalprice: 1200.00, address: '2020 Alder St', paymentmethod: 'Credit Card', ispaid: true
    },
    {
      userid: 6, shipid: 4, createat: knex.fn.now(), totalprice: 1250.00, address: '2121 Beech St', paymentmethod: 'Debit Card', ispaid: false
    },
    {
      userid: 7, shipid: 5, createat: knex.fn.now(), totalprice: 1300.00, address: '2222 Cedar St', paymentmethod: 'Credit Card', ispaid: true
    },
    {
      userid: 8, shipid: 6, createat: knex.fn.now(), totalprice: 1350.00, address: '2323 Elm St', paymentmethod: 'PayPal', ispaid: false
    },
    {
      userid: 9, shipid: 7, createat: knex.fn.now(), totalprice: 1400.00, address: '2424 Maple St', paymentmethod: 'Credit Card', ispaid: true
    },
    {
      userid: 10, shipid: 8, createat: knex.fn.now(), totalprice: 1450.00, address: '2525 Oak St', paymentmethod: 'Debit Card', ispaid: false
    },
    {
      userid: 11, shipid: 9, createat: knex.fn.now(), totalprice: 1500.00, address: '2626 Pine St', paymentmethod: 'Credit Card', ispaid: true
    },
    {
      userid: 12, shipid: 10, createat: knex.fn.now(), totalprice: 1550.00, address: '2727 Redwood St', paymentmethod: 'PayPal', ispaid: false
    }
  ]);
};
