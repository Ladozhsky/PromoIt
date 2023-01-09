import * as _ from "underscore";
import { Queries } from "../constants";
import { systemError, retweet } from "../entities";
import { SqlHelper } from "../helpers/sql.helper";
import { ErrorService } from "./error.service";

interface localRetweet {
  twitt_id: string;
  twitter_user_id: string;
  retweets: number;
  campaign: string;
  parsing_date: Date;
}

interface IRetweetService {
  getRetweets(): Promise<retweet[]>;
  getRetweetsByUserId(userId: number): Promise<retweet[]>;
  addRetweet(retweet: retweet): Promise<retweet>
}

export class RetweetService implements IRetweetService {
  private _errorService: ErrorService;

  constructor(private errorService: ErrorService) {
    this._errorService = errorService;
  }

  public getRetweets(): Promise<retweet[]> {
    return new Promise<retweet[]>((resolve, reject) => {
      const result: retweet[] = [];

      SqlHelper.executeQueryArrayResult<localRetweet>(
        this._errorService,
        Queries.Retweets
      )
        .then((queryResult: localRetweet[]) => {
          queryResult.forEach((retweet: localRetweet) => {
            result.push(this.parselocalRetweet(retweet));
          });

          resolve(result);
        })
        .catch((error: systemError) => {
          reject(error);
        });
    });
  }

  public getRetweetsByUserId(userId: number): Promise<retweet[]> {
    return new Promise<retweet[]>((resolve, reject) => {
      const result: retweet[] = [];

      SqlHelper.executeQueryArrayResult<localRetweet>(
        this._errorService,
        Queries.RetweetsByUserId,
        userId
      )
        .then((queryResult: localRetweet[]) => {
          queryResult.forEach((retweet: localRetweet) => {
            result.push(this.parselocalRetweet(retweet));
          });

          resolve(result);
        })
        .catch((error: systemError) => {
          reject(error);
        });
    });
  }

  public addRetweet(retweet: retweet): Promise<retweet> {
    return new Promise<retweet>((resolve, reject) => {
      SqlHelper.createNew(
        this._errorService,
        Queries.AddRetweet,
        retweet,
        retweet.twitt_id,
        retweet.twitter_user_id,
        retweet.retweets,
        retweet.campaign,
        retweet.parsing_date
      )
      .then((result: retweet) => {
        resolve(result);
        })
        .catch((error: systemError) => {
          reject(error);
        });
    });
  }

  private parselocalRetweet(local: localRetweet): retweet {
    return {
      twitt_id: local.twitt_id,
      twitter_user_id: local.twitter_user_id,
      retweets: local.retweets,
      campaign: local.campaign,
      parsing_date: local.parsing_date,
    };
  }
}
