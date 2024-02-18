import React, { useState } from 'react';
import './BingSearchComponent.css'; 


const BingSearchComponent = () => {
    const [searchResults, setSearchResults] = useState([]);
    const [query, setQuery] = useState('');
    const [isLoading, setIsLoading] = useState(false); 

    const handleSearch = async (event) => {
        event.preventDefault(); 
        setIsLoading(true);

        const apiKey = 'xxxxxxxx';
        const url = `https://api.bing.microsoft.com/v7.0/search?q=${query}&count=100`;
        
        try {
            const response = await fetch(url, {
                headers: {
                    'Ocp-Apim-Subscription-Key': apiKey
                }
            });
    
            if (!response.ok) {
                throw new Error('Network response was not ok');
            }
    
            const data = await response.json();
            let results = [];
    
            if (data.webPages && data.webPages.value) {
                results = [
                    ...results,
                    ...data.webPages.value.map(result => ({ type: 'Webpage', name: result.name, description: result.snippet, url: result.url }))
                ];
            }
    
            if (data.images && data.images.value) {
                results = [
                    ...results,
                    ...data.images.value.map(result => ({ type: 'Image', name: result.name, description: result.description, thumbnailUrl: result.thumbnailUrl, contentUrl: result.contentUrl }))
                ];
            }
    
            if (data.videos && data.videos.value) {
                results = [
                    ...results,
                    ...data.videos.value.map(result => ({ type: 'Video', name: result.name, description: result.description, contentUrl: result.contentUrl }))
                ];
            }
    
            setSearchResults(results);
        } catch (error) {
            console.error('Error fetching search results:', error);
        }finally {
            setIsLoading(false); // Set loading state to false when search is complete
        }
    };

    return (
        <div>
        <div className="search-container">           
                <input 
                    type="text"
                    className="search-input"
                    value={query}
                    onChange={(e) => setQuery(e.target.value)}
                />
                 <img src={process.env.PUBLIC_URL + './images/search.png'} alt="Search" className="search-icon" onClick={handleSearch} />
            </div>
            {isLoading && (
    <div className="loading-container">
        <div className="loading-indicator">Loading...</div>
    </div>
)}

            {!isLoading && (
                <div  className="search-results">
          
            <ul> 
                {searchResults.map((result, index) => (
                    <li key={index} className="search-result-item">
                        <h3>{result.name}</h3>
                        {result.description && <p>{result.description}</p>}
                        {result.type === 'Webpage' && <a href={result.url}>View Webpage</a>}
                        {result.type === 'Image' && <img src={result.thumbnailUrl} alt={result.name} onClick={() => window.open(result.contentUrl, '_blank')} className="search-result-image" />}
                        {result.type === 'Video' && <video controls src={result.contentUrl} className="search-result-video" />}
                    </li>
                ))}
            </ul>
        </div>)}
            </div>
    );
};

export default BingSearchComponent;