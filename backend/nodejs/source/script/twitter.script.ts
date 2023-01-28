import controller from "../controllers/twitter.controller";
import { ListCreation } from "../services/list-creation.services";
import { ErrorService } from '../services/error.service';
import * as cron from 'node-cron'
require('dotenv').config();

const errorService: ErrorService = new ErrorService();
const retweetList : ListCreation = new ListCreation(errorService)

// cron.schedule('48 19 * * *', () => {
//      addRetweets(retweetList.createListOfRetweetsTw())
//  })

controller.addRetweets(retweetList.createListOfRetweets())
