import { client } from "./twitterClient"
import { TweetEntityHashtagV2 } from "twitter-api-v2";

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
        `id: 1497659734115057666`, {
        'tweet.fields': 'public_metrics,author_id',
        expansions: 'author_id',
        'user.fields': 'username'
      })
  
    // Consume every possible tweet of jsTweets (until rate limit is hit)
    for await (const tweet of jsTweets) {
      console.log(tweet);
    }
  }

  // async function singleTweet() {
  //   const single = await client.v2.singleTweet(
  //       "1610712553553903621", {
  //       'tweet.fields': 'public_metrics,author_id,entities',
  //     })
  
  //     console.log(single.data.entities?.hashtags[0].tag)
  //   }


    
  //   singleTweet()