import * as _ from "underscore";
import { Queries } from "../constants";
import { systemError, twitterUserId } from "../entities";
import { SqlHelper } from "../helpers/sql.helper";
import { ErrorService } from "./error.service";


export interface ITwitterUserIds {
  twitter_user_id: string;
}
export interface ICanpaignHashtag {
  hashtag: string;
}

interface ITwitterUserIdService {
  getTwitterUserIds(): Promise<ITwitterUserIds[]>;
  getCanpaignHashtag(): Promise<ICanpaignHashtag[]>;
}

export class TwitterUserIdService implements ITwitterUserIdService {
  private _errorService: ErrorService;

  constructor(private errorService: ErrorService) {
    this._errorService = errorService;
  }

  public getTwitterUserIds(): Promise<ITwitterUserIds[]> {
    return new Promise<ITwitterUserIds[]>((resolve, reject) => {
      const result: ITwitterUserIds[] = [];

      SqlHelper.executeQueryArrayResult<ITwitterUserIds>(
        this._errorService,
        Queries.TwitterUserIds
      )
        .then((queryResult: ITwitterUserIds[]) => {
          queryResult.forEach((twitterUserId: ITwitterUserIds) => {
            result.push(twitterUserId);
          });

          resolve(result);
        })
        .catch((error: systemError) => {
          reject(error);
        });
    });
  }

  public getCanpaignHashtag(): Promise<ICanpaignHashtag[]> {
    return new Promise<ICanpaignHashtag[]>((resolve, reject) => {
      const result: ICanpaignHashtag[] = [];

      SqlHelper.executeQueryArrayResult<ICanpaignHashtag>(
        this._errorService,
        Queries.CampainHashtag
      )
        .then((queryResult: ICanpaignHashtag[]) => {
          queryResult.forEach((campainHashtag: ICanpaignHashtag) => {
            result.push(campainHashtag);
          });

          resolve(result);
        })
        .catch((error: systemError) => {
          reject(error);
        });
    });
  }
}
