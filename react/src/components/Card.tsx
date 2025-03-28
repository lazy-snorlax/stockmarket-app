import React, { SyntheticEvent } from 'react'
import { Link } from 'react-router-dom'
import { CompanySearch } from '../company';

interface Props {
    id:string;
    searchResult: CompanySearch;
    onPortfolioCreate: (e: SyntheticEvent) => void
}

const Card: React.FC<Props> = ({
    id,
    searchResult,
    onPortfolioCreate
}: Props): JSX.Element => {
  return (
    <div className="card bg-base-100 w-96 shadow-sm" key={id} id={id}>
      <div className="card-body items-center text-center">
        <h2 className="card-title">
          <Link to={`/company/${searchResult.symbol}/company-profile`} className="font-bold text-center text-veryDarkViolet md:text-left">
            {searchResult.name} ({searchResult.symbol})
          </Link>
        </h2>
        <p className="text-veryDarkBlue">{searchResult.currency}</p>
        <p className="font-bold text-veryDarkBlue">
            {searchResult.exchangeShortName} - {searchResult.stockExchange}
        </p>
        <div className="card-actions justify-end">
          {/* <AddPortfolio
            onPortfolioCreate={onPortfolioCreate}
            symbol={searchResult.symbol}
          /> */}
        </div>
      </div>
    </div>
    
  )
}

export default Card