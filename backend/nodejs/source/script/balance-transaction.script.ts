import addTransactions from "../controllers/balance-transaction.controller";
import { ErrorService } from '../services/error.service';
import { ListCreation } from "../services/list-creation.services";
import * as cron from 'node-cron'

const errorService: ErrorService = new ErrorService();
const transactionList : ListCreation = new ListCreation(errorService)

// cron.schedule('18 18 * * *', () => {
//     addRetweets(retweetList.createListOfRetweets())
// })


addTransactions(transactionList.createListOfTransaction())
