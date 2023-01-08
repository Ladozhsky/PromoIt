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
        `#PromoIt from:1606972070222565382`, {
        'tweet.fields': 'public_metrics,author_id',
      })
  
    // Consume every possible tweet of jsTweets (until rate limit is hit)
    for await (const tweet of jsTweets) {
      console.log(tweet);
    }
  }

  consumeTweets()