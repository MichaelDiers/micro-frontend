const functions = require('firebase-functions'); // eslint-disable-line no-unused-vars

const app = require('./app/app');

exports.frame = functions.https.onRequest(app());
