
import {DbGetService, ITwitterUserIds, ICanpaignHashtag} from '../services/twitterParsing.services';
import { ErrorService } from '../services/error.service';
import { systemError, retweet } from '../entities';
import { client} from "./twitterClient"
import { TweetV2, TweetSearchRecentV2Paginator} from "twitter-api-v2";
import { RetweetService } from '../services/retweet.services';
import { Request, Response, NextFunction } from 'express';
import { ResponseHelper } from '../helpers/response.helper';

const errorService: ErrorService = new ErrorService();
const retweetService: RetweetService = new RetweetService(errorService);
const dbGetService: DbGetService = new DbGetService(errorService);

async function processTwitterData() {

            
      //get Ids and Hashtags for parsing
      const userIds : ITwitterUserIds[] = await dbGetService.getTwitterUserIds();
      const hashtag : ICanpaignHashtag[] = await dbGetService.getCanpaignHashtag();

      let retweetArray : retweet[] = [];

      // Use created list of Ids and Hashtags to create list of retweets
      async function createListOfRetweets () {
        for (let i = 0; i < userIds.length; i++) {
          for (let j = 0; j < hashtag.length; j++) {
            await searchTweetsByIdHash(userIds[i].twitter_user_id, hashtag[j].hashtag)
            }
        }
        return retweetArray
      }

      // Search Tweets using Ids and Hashtags Metod and convertion to retweet type
      
      async function searchTweetsByIdHash(userId : string , hashtag : string) {
        let twitterQuery : string = `${hashtag} from:${userId}`;

        const jsTweets : TweetSearchRecentV2Paginator = await client.v2.search(
            twitterQuery, {
            'tweet.fields': 'public_metrics,author_id,referenced_tweets',
          })
      
        for (const tweet of jsTweets) {
          if(tweet?.referenced_tweets != undefined) {
            let retweetedCheck : boolean = false
            for (const referencedTweet of tweet.referenced_tweets) {
              if(referencedTweet.type === "retweeted") {
                retweetedCheck = true
              }
            }
            if (retweetedCheck === false){
              retweetArray.push(await parselocalRetweet(tweet, hashtag))
            }   
          }
          else retweetArray.push(await parselocalRetweet(tweet, hashtag))
        }

        return retweetArray
      }
  
      
      
      const addRetweets = async (retweetList: retweet[]) => {
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

      const retweetList : retweet[] = await createListOfRetweets()
      
      addRetweets(retweetList)
}


  processTwitterData()



async function parselocalRetweet(local: TweetV2, hashtag : string): Promise<retweet> {
      return {
        twitt_id: local.id,
        twitter_user_id: local.author_id as string,
        retweets: local.public_metrics?.retweet_count as number,
        campaign: hashtag,
        parsing_date: new Date(2023.01),
      };
    }