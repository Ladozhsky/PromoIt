import { systemError, retweet } from '../entities';
import { ErrorService } from '../services/error.service';
import { RetweetService } from '../services/twitter-related.setvises/retweet.services';
import {NON_EXISTENT_ID} from "../constants"

const errorService: ErrorService = new ErrorService();
const retweetService: RetweetService = new RetweetService(errorService);

const addRetweets = async (retweetListInput: Promise<retweet[]>) => {
  const retweetList : retweet[] = await retweetListInput
  for (let i = 0; i < retweetList.length; i++) {
      retweetService.addRetweet({
        twitt_id: retweetList[i].twitt_id,
        twitter_user_id: retweetList[i].twitter_user_id,
        campaign: retweetList[i].campaign,
        retweets: retweetList[i].retweets,
        parsing_date: new Date(),
        creation_date: new Date(),
        update_date: new Date(),
        create_by_user: retweetList[i].create_by_user,
        update_by_user: retweetList[i].update_by_user,
        status: retweetList[i].status,
      })
        .then(() => {
          return console.log(retweetList[i]);
      })
        .catch((error: systemError) => {
          return console.log(error);
      });
    }
  }

  export default (addRetweets)
