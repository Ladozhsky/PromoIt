/** source/server.ts */
import http from 'http';
import express, { Express } from 'express';
import morgan from 'morgan';
require('dotenv').config();
import { StudentDB } from './database/student.db';
import { Student } from './database/models/student.model';
import twitterRoutes from './routes/twitter.routes';


const router: Express = express();

/** Logging */
router.use(morgan('dev'));
/** Parse the request */
router.use(express.urlencoded({ extended: false }));
/** Takes care of JSON data */
router.use(express.json());

/** RULES OF OUR API */
router.use((req, res, next) => {
    // set the CORS policy
    res.header('Access-Control-Allow-Origin', '*');
    // set the CORS headers
    res.header('Access-Control-Allow-Headers', 'origin, X-Requested-With,Content-Type,Accept, Authorization');
    // set the CORS method headers
    if (req.method === 'OPTIONS') {
        res.header('Access-Control-Allow-Methods', 'GET PATCH DELETE POST');
        return res.status(200).json({});
    }
    next();
});

/** Routes */

router.use('/twitter/', twitterRoutes.router);

router.use('/students', (req, res) => {
    res.json(StudentDB);
});

/** Error handling */
router.use((req, res, next) => {
    const error = new Error('not found');
    return res.status(404).json({
        message: error.message
    });
});

router.post('/data', (req, res) => {
    const student = new Student(req.body);
    student.save((err: any) => {
      if (err) return res.status(400).send(err);
      res.status(200).send(student);
    });
  });

/** Server */
const httpServer = http.createServer(router);
const PORT: any = process.env.PORT ?? 5555;
httpServer.listen(
    PORT,
    () => {
        console.log(`The server is running on port ${PORT}`);
    });