import addRetweets from "../controllers/twitter.controllet";
import { ListCreation } from "../services/list-creation.services";
import * as cron from 'node-cron'

const retweetList : ListCreation = new ListCreation

// cron.schedule('48 19 * * *', () => {
//      addRetweets(retweetList.createListOfRetweetsTw())
//  })

addRetweets(retweetList.createListOfRetweetsTw())
