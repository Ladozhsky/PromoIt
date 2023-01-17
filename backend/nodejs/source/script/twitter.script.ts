import addRetweets from "../controllers/twitter.controller";
import { ListCreation } from "../services/list-creation.services";
import { ErrorService } from '../services/error.service';
import * as cron from 'node-cron'

const errorService: ErrorService = new ErrorService();
const retweetList : ListCreation = new ListCreation(errorService)

// cron.schedule('48 19 * * *', () => {
//      addRetweets(retweetList.createListOfRetweetsTw())
//  })

addRetweets(retweetList.createListOfRetweets())
