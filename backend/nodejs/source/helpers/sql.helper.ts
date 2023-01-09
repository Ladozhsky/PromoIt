import { Connection, SqlClient, Error, Query, ProcedureManager } from "msnodesqlv8";
import { DB_CONNECTION_STRING, Queries } from "../constants";
import { systemError } from "../entities";
import { AppError } from "../enums";
import { ErrorService } from "../services/error.service";

export class SqlHelper {
    static sql: SqlClient = require("msnodesqlv8");

    public static executeQueryArrayResult<T>(errorService: ErrorService, query: string, ...params: (string | number| Date)[]): Promise<T[]> {
        return new Promise<T[]>((resolve, reject) => {

            SqlHelper.openConnection(errorService)
                .then((connection: Connection) => {
                    connection.query(query, params, (queryError: Error | undefined, queryResult: T[] | undefined) => {
                        if (queryError) {
                            reject(errorService.getError(AppError.QueryError));
                        }
                        else {
                            if (queryResult !== undefined) {
                                resolve(queryResult);
                            }
                            else {
                                resolve([]);
                            }
                        }
                    });
                })
                .catch((error: systemError) => {
                    reject(error);
                });
        });
    }

    public static createNew<T>(errorService: ErrorService, query: string, original: T, ...params: (string | number | Date )[]): Promise<T> {
        return new Promise<T>((resolve, reject) => {
            SqlHelper.openConnection(errorService)
                .then((connection: Connection) => {
                    const queries: string[] = [query, Queries.SelectIdentity];
                    const executedQuery: string = queries.join(";");
                    let executionCounter: number = 0;
                    connection.query(executedQuery, params, (queryError: Error | undefined, queryResult: T[] | undefined) => {
                        if (queryError) {
                            reject(errorService.getError(AppError.QueryError));
                        }
                        else {
                            executionCounter++;
                            const badQueryError: systemError = errorService.getError(AppError.QueryError);

                            if (executionCounter === queries.length) {
                                if (queryResult !== undefined) {
                                    if (queryResult.length === 1) {
                                        (original as any).id = (queryResult[0] as any).id;
                                        resolve(original);
                                    }
                                    else {
                                        reject(badQueryError);
                                    }
                                }
                                else {
                                    reject(badQueryError);
                                }
                            }
                        }
                    });
                })
                .catch((error: systemError) => {
                    reject(error);
                })
        });
    }
    


    private static openConnection(errorService: ErrorService): Promise<Connection> {
        return new Promise<Connection>((resolve, reject) => {
            SqlHelper.sql.open(DB_CONNECTION_STRING, (connectionError: Error, connection: Connection) => {
                if (connectionError) {
                    reject(errorService.getError(AppError.ConnectionError));
                }
                else {
                    resolve(connection)
                }
            });
        });
    }
}