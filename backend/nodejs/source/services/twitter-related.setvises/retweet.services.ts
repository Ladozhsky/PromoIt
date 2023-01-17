import * as _ from "underscore";
import { Queries } from "../../constants";
import { systemError, retweet, transaction } from '../../entities';
import { SqlHelper } from "../../helpers/sql.helper";
import { ErrorService } from "../error.service";
import { DateHelper } from "../../helpers/date.helper";


interface localRetweet {
  retweet_id: number;
  twitt_id: string;
  twitter_user_id: string ;
  campaign_id: number;    
  retweets: number;
  parsing_date: Date;
  creation_date: Date;
  update_date: Date;
  create_by_user: string;   
  update_by_user: string;   
  status: number;   
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

  public updateRetweetByScript(retweet_id: number): Promise<string> {
    return new Promise<string>((resolve, reject) => {
        const updateDate: Date = new Date();
        const sucsessfulUpdate: string = `status for retweet ${retweet_id}sucsessfuly changed to 2`;
        SqlHelper.executeQueryNoResult(this.errorService, Queries.UpdateRetweet, false, 2, retweet_id)
            .then(() => {
                resolve(sucsessfulUpdate);
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
        retweet.campaign_id,
        retweet.retweets,
        retweet.parsing_date,
        retweet.creation_date,
        retweet.update_date,
        retweet.create_by_user,
        retweet.update_by_user,
        retweet.status,
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
      campaign_id: local.campaign_id,
      parsing_date: local.parsing_date,
      creation_date: local.creation_date,
      update_date: local.update_date,
      create_by_user: local.create_by_user,
      update_by_user: local.update_by_user,
      status: local.status,
    };
  }
}
