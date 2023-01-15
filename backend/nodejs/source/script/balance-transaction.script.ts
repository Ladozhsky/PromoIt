import addTransactions from "../controllers/balance-transaction.controller";
import { ListCreation } from "../services/twitter-related.setvises/list-creation.services";
import * as cron from 'node-cron'

const transactionList : ListCreation = new ListCreation

// cron.schedule('18 18 * * *', () => {
//     addRetweets(retweetList.createListOfRetweets())
// })

addTransactions(transactionList.createListOfTransactionDemo())
