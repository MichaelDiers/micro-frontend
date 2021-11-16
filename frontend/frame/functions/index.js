const functions = require('firebase-functions');

const app = require('./app/app');
const appConfig = require('./app-config');

exports.framefe = functions.https.onRequest(app(appConfig()));
