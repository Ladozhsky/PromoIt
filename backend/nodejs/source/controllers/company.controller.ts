import { Request, Response, NextFunction } from 'express';
import { systemError, company } from '../entities';
import { RequestHelper } from '../helpers/request.helper';
import { ResponseHelper } from '../helpers/response.helper';
import { ErrorService } from '../services/error.service';
import { CompanyService } from '../services/company.services';

const errorService: ErrorService = new ErrorService();
const companyService: CompanyService = new CompanyService(errorService);

const getCompanies = async (req: Request, res: Response, next: NextFunction) => {
    companyService.getCompanies()
        .then((result: company[]) => {
            return res.status(200).json({
                companies: result
            });
        })
        .catch((error: systemError) => {
            return ResponseHelper.handleError(res, error);
        });
};

// const addCompany = async (req: Request, res: Response, next: NextFunction) => {
//     const body: company = req.body;

//     companyService.addCompany({
//         company_name: body.company_name,
//         site: body.site,
//         email: body.email,
//         company_type: body.company_type
//     })
//         .then((result: company) => {
//             return res.status(200).json(result);
//         })
//         .catch((error: systemError) => {
//             return ResponseHelper.handleError(res, error);
//         });
// };

export default { getCompanies }