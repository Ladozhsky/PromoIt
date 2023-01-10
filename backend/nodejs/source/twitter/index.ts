import { client } from "./twitterClient"
import { TweetEntityHashtagV2, ReferencedTweetV2, TweetV2 } from "twitter-api-v2";

async function tweet(): Promise<void> {
  try {
    await client.v2.tweet("PromoIt test post #PromoIt");
  } catch (e) {
    console.error(e);
  }
}


let checkedHashtag = "#PromoIt"
let activistId = "1609248785141481475"
// from: ${activistId}

let twitterQuery = `#PromoIt`

async function consumeTweets() {
    const jsTweets = await client.v2.search(
        `from:1606972070222565382`, {
        'tweet.fields': 'public_metrics,author_id,referenced_tweets',
      })
  
    // Consume every possible tweet of jsTweets (until rate limit is hit)
    for (const tweet of jsTweets) {
      if(tweet?.referenced_tweets != undefined) {
        let retweetedCheck : boolean = false
        for (const referencedTweet of tweet.referenced_tweets) {
          if(referencedTweet.type === "retweeted") {
            retweetedCheck = true
          }
        }
        if (retweetedCheck === false){
          console.log(tweet)
        }   
      }
      else console.log(tweet)
    };
  }


  consumeTweets()


