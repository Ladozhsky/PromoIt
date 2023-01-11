import { systemError, retweet } from '../entities';
import { ErrorService } from '../services/error.service';
import { RetweetService } from '../services/retweet.services';


const errorService: ErrorService = new ErrorService();
const retweetService: RetweetService = new RetweetService(errorService);

const addRetweets = async (retweetListInput: Promise<retweet[]>) => {
  const retweetList : retweet[] = await retweetListInput
  for (let i = 0; i < retweetList.length; i++) {
      retweetService.addRetweet({
        twitt_id: retweetList[i].twitt_id,
        twitter_user_id: retweetList[i].twitter_user_id,
        retweets: retweetList[i].retweets,
        campaign: retweetList[i].campaign,
        parsing_date: retweetList[i].parsing_date})

        .then(() => {
          return console.log(retweetList[i]);
      })
        .catch((error: systemError) => {
          return console.log(error);
      });
    }
  }

  export default (addRetweets)