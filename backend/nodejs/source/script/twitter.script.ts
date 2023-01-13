import addRetweets from "../controllers/twitter.controllet";
import { ListCreation } from "../services/list-creation.services";
import * as cron from 'node-cron'

const retweetList : ListCreation = new ListCreation

// cron.schedule('18 18 * * *', () => {
//     addRetweets(retweetList.createListOfRetweets())
// })

addRetweets(retweetList.createListOfRetweets())
