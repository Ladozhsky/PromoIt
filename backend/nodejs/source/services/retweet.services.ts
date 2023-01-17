import * as _ from "underscore";
import { Queries } from "../constants";
import { systemError, retweet } from '../entities';
import { SqlHelper } from "../helpers/sql.helper";
import { ErrorService } from "./error.service";

interface IRetweetService {
  addRetweet(retweet: retweet): Promise<retweet>
}

export class RetweetService implements IRetweetService {
  private _errorService: ErrorService;

  constructor(private errorService: ErrorService) {
    this._errorService = errorService;
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
}

