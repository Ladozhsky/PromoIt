
import {TwitterUserIdService, ITwitterUserIds, ICanpaignHashtag} from '../services/twitterParsing.services';
import { ErrorService } from '../services/error.service';
import { twitterUserId } from '../entities';
import { client } from "./twitterClient"

const errorService: ErrorService = new ErrorService();
const retweetService: TwitterUserIdService = new TwitterUserIdService(errorService);

async function processTwitterData() {

      const userIds : ITwitterUserIds[] = await retweetService.getTwitterUserIds();
      const hashtag : ICanpaignHashtag[] = await retweetService.getCanpaignHashtag();
      

      
      console.log(`${hashtag[0].hashtag} from:${userIds[0].twitter_user_id}`);
      
      async function consumeTweets(userId : string , hashtag : string) {
        let twitterQuery = `${hashtag} from:${userId}`;
        const jsTweets = await client.v2.search(
            twitterQuery, {
            'tweet.fields': 'public_metrics,author_id',
          })
      
        for await (const tweet of jsTweets) {
          console.log(tweet);
        
        }
      }
    
      for (let i = 0; i < userIds.length; i++) {
        for (let j = 0; j < hashtag.length; j++) {
          await consumeTweets(userIds[i].twitter_user_id, hashtag[j].hashtag);
        }
      }
    }
    
 
  processTwitterData()

// async function twitterParsing() {
//     const jsTweets = await 
//         for (let i = 0; i < twitterUserIds.length; index++) {
//         const element = array[index];

//     } client.v2.search(
//         `id: 1497659734115057666`, {
//         'tweet.fields': 'public_metrics,author_id',
//         expansions: 'author_id',
//         'user.fields': 'username'
//       })
  
//     // Consume every possible tweet of jsTweets (until rate limit is hit)
//     for await (const tweet of jsTweets) {
//       console.log(tweet);
//     }
// }


// dbParsing()

    
