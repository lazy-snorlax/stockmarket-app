import { SyntheticEvent, useState } from "react"
import CardList from "../components/CardList"
import Search from "../components/Search"
import { CompanySearch } from "../company"
import { searchCompanies } from "../api"
import { PortfolioGet } from "../models/Portfolio"
import { portfolioAddAPI } from "../services/PortfolioService"

const SearchPage = () => {
    const [search, setSearch] = useState<string>("")
    const [portfolioValues, setPortfolioValues] = useState<PortfolioGet[] | null>([])
    const [searchResult, setSearchResult] = useState<CompanySearch[]>([])
    const [serverError, setServerError] = useState<string | null>(null)

    const handleSearchChange = (e: ChangeEvent<HTMLInputElement>) => {
        setSearch(e.target.value);
    };

    const onPortfolioCreate = (e: any) => {
        e.preventDefault();
        console.log(">>> creating portfolio", e)
        portfolioAddAPI(e.target[0].value)
            .then((res) => {
                if (res?.data) {
                    setPortfolioValues(res?.data)
                }
            }).catch((e) => {
                setPortfolioValues(null)
            });
    };

    const onSearchSubmit = async (e: SyntheticEvent) => {
        e.preventDefault();
        console.log(">>> searching for: ", search)
        const result = await searchCompanies(search)
        if (typeof result === "string") {
            setServerError(result)
        } else if (Array.isArray(result.data)) {
            setSearchResult(result.data)
        }
    };

    return (
        <div className=" bg-base-200 min-h-screen">
            <div className="text-center lg:text-left">
                <Search 
                    onSearchSubmit={onSearchSubmit} 
                    search={search}
                    handleSearchChange={handleSearchChange}
                />
            </div>
            <div className="hero">
                <div className="hero-content">
                    <div className="">
                        {serverError && <div>Unable to connect to API</div>}
                        <CardList 
                            searchResults={searchResult} 
                            onPortfolioCreate={onPortfolioCreate} 
                            />
                    </div>
                </div>
            </div>
        </div>
    )
}

export default SearchPage